import {bindValue, trigger} from "cs2/api";
import mod from "mod.json";
import {TimeWeatherProfile} from "./domain/timeWeatherProfile";

const MAIN_PANEL_OPEN = "MainPanelOpen"
export const PROFILES = "Profiles"
export const SELECTED_PROFILE = "SelectedProfile"
export const CREATE_PROFILE = "CreateProfile"
export const UPDATE_PROFILE = "UpdateProfile"
export const DELETE_PROFILE = "DeleteProfile"
export const CUSTOM_TIME = "CurrentTime"
export const CUSTOM_TEMPERATURE = "CurrentTemperature"
export const TIME_OPTION = "TimeOption"
export const WEATHER_OPTION = "WeatherOption"
export const ENABLE_CUSTOM_PRECIPITATION = "EnableCustomPrecipitation"
export const ENABLE_CUSTOM_CLOUDS = "EnableCustomClouds"
export const ENABLE_CUSTOM_AURORA = "EnableCustomAurora"
export const ENABLE_CUSTOM_TEMPERATURE = "EnableCustomTemperature"
export const ENABLE_CUSTOM_FOG = "EnableCustomFog"
export const ENABLE_CUSTOM_THUNDER = "EnableCustomThunder"
export const CUSTOM_DAY_OF_THE_YEAR = "CurrentDayOfTheYear"
export const CUSTOM_PRECIPITATION = "CurrentPrecipitation"
export const CUSTOM_CLOUDS = "CurrentClouds"
export const CUSTOM_AURORA = "CurrentAurora"
export const CUSTOM_WEATHER_TIME = "CurrentWeatherTime"
export const CUSTOM_FOG = "CurrentFog"
export const CUSTOM_THUNDER = "CurrentThunder"
export const CUSTOM_RAINBOW = "CurrentRainbow"
export const CUSTOM_LATITUDE = "CustomLatitude"
export const CUSTOM_LONGITUDE = "CustomLongitude"

export const TIME_PREFERENCE = "TimePreference"
export const TEMPERATURE_PREFERENCE = "TemperaturePreference"

export const MainPanelOpen = bindValue<boolean>(mod.id, MAIN_PANEL_OPEN);
export const CurrentTime = bindValue<number>(mod.id, CUSTOM_TIME);
export const Profiles = bindValue<TimeWeatherProfile[]>(mod.id, PROFILES);
export const SelectedProfile = bindValue<string>(mod.id, SELECTED_PROFILE);
export const CurrentTemperature = bindValue<number>(mod.id, CUSTOM_TEMPERATURE);
export const TimeOption = bindValue<number>(mod.id, TIME_OPTION);
export const WeatherOption = bindValue<number>(mod.id, WEATHER_OPTION);
export const EnableCustomPrecipitation = bindValue<boolean>(mod.id, ENABLE_CUSTOM_PRECIPITATION);
export const EnableCustomClouds = bindValue<boolean>(mod.id, ENABLE_CUSTOM_CLOUDS);
export const EnableCustomAurora = bindValue<boolean>(mod.id, ENABLE_CUSTOM_AURORA);
export const EnableCustomFog = bindValue<boolean>(mod.id, ENABLE_CUSTOM_FOG);
export const EnableCustomThunder = bindValue<boolean>(mod.id, ENABLE_CUSTOM_THUNDER);
export const CurrentDayOfTheYear = bindValue<number>(mod.id, CUSTOM_DAY_OF_THE_YEAR);
export const CurrentPrecipitation = bindValue<number>(mod.id, CUSTOM_PRECIPITATION);
export const CurrentClouds = bindValue<number>(mod.id, CUSTOM_CLOUDS);
export const CurrentAurora = bindValue<number>(mod.id, CUSTOM_AURORA);
export const CurrentWeatherTime = bindValue<number>(mod.id, CUSTOM_WEATHER_TIME);
export const CustomFog = bindValue<number>(mod.id, CUSTOM_FOG);
export const CustomThunder = bindValue<number>(mod.id, CUSTOM_THUNDER);
export const CustomRainbow = bindValue<number>(mod.id, CUSTOM_RAINBOW);
export const EnableCustomTemperature = bindValue<boolean>(mod.id, ENABLE_CUSTOM_TEMPERATURE);
export const CustomLatitude = bindValue<number>(mod.id, CUSTOM_LATITUDE);
export const CustomLongitude = bindValue<number>(mod.id, CUSTOM_LONGITUDE);

export const TemperaturePreferenceValueBinding = bindValue<number>(mod.id, TEMPERATURE_PREFERENCE)
export const TimePreferenceValueBinding = bindValue<number>(mod.id, TIME_PREFERENCE)

export const SetMainPanelOpen = (open: boolean) => trigger(mod.id, MAIN_PANEL_OPEN, open);
export const SetCurrentTime = (time: number) => trigger(mod.id, CUSTOM_TIME, time);
export const SetCurrentTemperature = (time: number) => trigger(mod.id, CUSTOM_TEMPERATURE, time);
export const SetTimeOption = (option: number) => trigger(mod.id, TIME_OPTION, option);
export const SetWeatherOption = (option: number) => trigger(mod.id, WEATHER_OPTION, option);
export const SetEnableCustomPrecipitation = (enabled: boolean) => trigger(mod.id, ENABLE_CUSTOM_PRECIPITATION, enabled);
export const SetEnableCustomFog = (enabled: boolean) => trigger(mod.id, ENABLE_CUSTOM_FOG, enabled);
export const SetEnableCustomThunder = (enabled: boolean) => trigger(mod.id, ENABLE_CUSTOM_THUNDER, enabled);
export const SetCustomPrecipitation = (value: number) => trigger(mod.id, CUSTOM_PRECIPITATION, value);
export const SetEnableCustomClouds = (enabled: boolean) => trigger(mod.id, ENABLE_CUSTOM_CLOUDS, enabled);
export const SetCustomClouds = (value: number) => trigger(mod.id, CUSTOM_CLOUDS, value);
export const SetEnableCustomAurora = (enabled: boolean) => trigger(mod.id, ENABLE_CUSTOM_AURORA, enabled);
export const SetCustomAurora = (value: number) => trigger(mod.id, CUSTOM_AURORA, value);
export const SetCustomDayOfTheYear = (value: number) => trigger(mod.id, CUSTOM_DAY_OF_THE_YEAR, value);
export const SetEnableCustomTemperature = (enabled: boolean) => trigger(mod.id, ENABLE_CUSTOM_TEMPERATURE, enabled);
export const SetCustomWeatherTime = (value: number) => trigger(mod.id, CUSTOM_WEATHER_TIME, value);
export const SetCustomFog = (value: number) => trigger(mod.id, CUSTOM_FOG, value);
export const SetCustomThunder = (value: number) => trigger(mod.id, CUSTOM_THUNDER, value);
export const SetCustomRainbow = (value: number) => trigger(mod.id, CUSTOM_RAINBOW, value);
export const SetSelectedProfile = (id: string) => trigger(mod.id, SELECTED_PROFILE, id);
export const CreateProfile = (name: string, copyCurrentProfile: boolean) => trigger(mod.id, CREATE_PROFILE, name, copyCurrentProfile);
export const DeleteProfile = (id: string) => trigger(mod.id, DELETE_PROFILE, id);
export const UpdateProfile = (id: string, name: string) => trigger(mod.id, UPDATE_PROFILE, id, name);
export const SetCustomLatitude = (value: number) => trigger(mod.id, CUSTOM_LATITUDE, value);
export const SetCustomLongitude = (value: number) => trigger(mod.id, CUSTOM_LONGITUDE, value);