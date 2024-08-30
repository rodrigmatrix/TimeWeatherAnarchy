import { bindValue, trigger } from "cs2/api";
import mod from "mod.json";

export const TimeOptions = {
    Default: 0,
    Day: 1,
    Night: 2,
    Custom: 3,
} as const;

export const WeatherOptions = {
    Default: 0,
    Spring: 1,
    Summer: 2,
    Fall: 3,
    Winter: 4,
    Custom: 5,
} as const;

const MAIN_PANEL_OPEN = "MainPanelOpen"
export const CUSTOM_TIME = "CurrentTime"
export const CUSTOM_TEMPERATURE = "CurrentTemperature"
export const TIME_OPTION = "TimeOption"
export const WEATHER_OPTION = "WeatherOption"
export const ENABLE_CUSTOM_PRECIPITATION = "EnableCustomPrecipitation"
export const ENABLE_CUSTOM_CLOUDS = "EnableCustomClouds"
export const ENABLE_CUSTOM_AURORA = "EnableCustomAurora"
export const ENABLE_CUSTOM_TEMPERATURE = "EnableCustomTemperature"
export const CUSTOM_DAY_OF_THE_YEAR = "CurrentDayOfTheYear"
export const CUSTOM_PRECIPITATION = "CurrentPrecipitation"
export const CUSTOM_CLOUDS = "CurrentClouds"
export const CUSTOM_AURORA = "CurrentAurora"
export const CUSTOM_WEATHER_TIME = "CurrentWeatherTime"

export const MainPannelOpen = bindValue<boolean>(mod.id, MAIN_PANEL_OPEN);
export const CurrentTime = bindValue<number>(mod.id, CUSTOM_TIME);
export const CurrentTemperature = bindValue<number>(mod.id, CUSTOM_TEMPERATURE);
export const TimeOption = bindValue<number>(mod.id, TIME_OPTION);
export const WeatherOption = bindValue<number>(mod.id, WEATHER_OPTION);
export const EnableCustomPrecipitation = bindValue<boolean>(mod.id, ENABLE_CUSTOM_PRECIPITATION);
export const EnableCustomClouds = bindValue<boolean>(mod.id, ENABLE_CUSTOM_CLOUDS);
export const EnableCustomAurora = bindValue<boolean>(mod.id, ENABLE_CUSTOM_AURORA);
export const CurrentDayOfTheYear = bindValue<number>(mod.id, CUSTOM_DAY_OF_THE_YEAR);
export const CurrentPrecipitation = bindValue<number>(mod.id, CUSTOM_PRECIPITATION);
export const CurrentClouds = bindValue<number>(mod.id, CUSTOM_CLOUDS);
export const CurrentAurora = bindValue<number>(mod.id, CUSTOM_AURORA);
export const CurrentWeatherTime = bindValue<number>(mod.id, CUSTOM_WEATHER_TIME);
export const EnableCustomTemperature = bindValue<boolean>(mod.id, ENABLE_CUSTOM_TEMPERATURE);

export const SetMainPanelOpen = (open: boolean) => trigger(mod.id, MAIN_PANEL_OPEN, open);
export const SetCurrentTime = (time: number) => trigger(mod.id, CUSTOM_TIME, time);
export const SetCurrentTemperature = (time: number) => trigger(mod.id, CUSTOM_TEMPERATURE, time);
export const SetTimeOption = (option: number) => trigger(mod.id, TIME_OPTION, option);
export const SetWeatherOption = (option: number) => trigger(mod.id, WEATHER_OPTION, option);
export const SetEnableCustomPrecipitation = (enabled: boolean) => trigger(mod.id, ENABLE_CUSTOM_PRECIPITATION, enabled);
export const SetCustomPrecipitation = (value: number) => trigger(mod.id, CUSTOM_PRECIPITATION, value);
export const SetEnableCustomClouds = (enabled: boolean) => trigger(mod.id, ENABLE_CUSTOM_CLOUDS, enabled);
export const SetCustomClouds = (value: number) => trigger(mod.id, CUSTOM_CLOUDS, value);
export const SetEnableCustomAurora = (enabled: boolean) => trigger(mod.id, ENABLE_CUSTOM_AURORA, enabled);
export const SetCustomAurora = (value: number) => trigger(mod.id, CUSTOM_AURORA, value);
export const SetCustomDayOfTheYear = (value: number) => trigger(mod.id, CUSTOM_DAY_OF_THE_YEAR, value);
export const SetEnableCustomTemperature = (enabled: boolean) => trigger(mod.id, ENABLE_CUSTOM_TEMPERATURE, enabled);
export const SetCustomWeatherTime = (value: number) => trigger(mod.id, CUSTOM_WEATHER_TIME, value);