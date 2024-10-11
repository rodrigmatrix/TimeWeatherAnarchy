﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Colossal.UI.Binding;
using Unity.Entities;
using UnityEngine;

namespace TimeWeatherAnarchy.Code.System;


public class GenericUIReader<T> : IReader<T>
{
    private static readonly Dictionary<Type, object> _readers = (Dictionary<Type, object>)typeof(ValueReaders).GetField("s_Readers", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

    public static IReader<T> Create()
    {
        var type = typeof(T);

        return (IReader<T>)Create(type);
    }

    private static object Create(Type type)
    {
        if (_readers.TryGetValue(type, out var valueReader))
        {
            return valueReader;
        }

        if (typeof(IJsonReadable).IsAssignableFrom(type))
        {
            return Activator.CreateInstance(typeof(ValueReader<>).MakeGenericType(type));
        }

        return _readers[type] = new GenericUIReader<T>();
    }

    public void Read(IJsonReader reader, out T value)
    {
        value = (T)ReadGeneric(reader, typeof(T));
    }

    private static object ReadGeneric(IJsonReader reader, Type type)
    {
        if (type.IsAssignableFrom(typeof(IJsonReadable)))
        {
            var value = (IJsonReadable)Activator.CreateInstance(type);

            value.Read(reader);

            return value;
        }

        if (type == typeof(int))
        {
            reader.Read(out int val);

            return val;
        }

        if (type == typeof(bool))
        {
            reader.Read(out bool val);

            return val;
        }

        if (type == typeof(uint))
        {
            reader.Read(out uint val);

            return val;
        }

        if (type == typeof(float))
        {
            reader.Read(out float val);

            return val;
        }

        if (type == typeof(double))
        {
            reader.Read(out double val);

            return val;
        }

        if (type == typeof(string))
        {
            reader.Read(out string val);

            return val;
        }

        if (type.IsEnum)
        {
            reader.Read(out int val);

            return val;
        }

        if (type == typeof(Entity))
        {
            reader.Read(out Entity val);

            return val;
        }

        if (type == typeof(Color))
        {
            reader.Read(out Color val);

            return val;
        }

        if (type.IsArray)
        {
            var length = (int)reader.ReadArrayBegin();
            var array = (Array)Activator.CreateInstance(type, length);

            for (var i = 0; i < length; i++)
            {
                reader.ReadArrayElement((ulong)i);

                array.SetValue(ReadGeneric(reader, type.GetElementType()), i);
            }

            reader.ReadArrayEnd();

            return array;
        }

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
        {
            var length = reader.ReadArrayBegin();
            var genericListType = typeof(List<>).MakeGenericType(type.GenericTypeArguments[0]);
            var genericList = (IList)Activator.CreateInstance(genericListType);

            for (var i = 0ul; i < length; i++)
            {
                reader.ReadArrayElement(i);

                genericList.Add(ReadGeneric(reader, type.GenericTypeArguments[0]));
            }

            reader.ReadArrayEnd();

            return genericList;
        }

        return ReadObject(reader, type);
    }

    private static object ReadObject(IJsonReader reader, Type type)
    {
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
        var obj = Activator.CreateInstance(type);

        reader.ReadMapBegin();

        foreach (var propertyInfo in properties)
        {
            if (!propertyInfo.HasAttribute<ReaderIgnoreAttribute>() && reader.ReadProperty(propertyInfo.Name))
            {
                propertyInfo.SetValue(obj, ReadGeneric(reader, propertyInfo.PropertyType));
            }
        }

        foreach (var fieldInfo in fields)
        {
            if (!fieldInfo.HasAttribute<ReaderIgnoreAttribute>() && reader.ReadProperty(fieldInfo.Name))
            {
                fieldInfo.SetValue(obj, ReadGeneric(reader, fieldInfo.FieldType));
            }
        }

        reader.ReadMapEnd();

        return obj;
    }
}

public class ReaderIgnoreAttribute : Attribute { }
public class WriterIgnoreAttribute : Attribute { }