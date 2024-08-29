import { bindValue, trigger } from "cs2/api";
import mod from "mod.json";

// export enum TimeOptions {
//     Default = 0,
//     Day = 1,
//     Night = 2,
//     Custom = 3,
// }

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
    Autumn: 3,
    Winter: 4,
    Custom: 5,
} as const;

const MAIN_PANNEL_OPEN = "MainPannelOpen"
export const CURRENT_TIME = "CurrentTime"
export const CURRENT_TEMPERATURE = "CurrentTemperature"
export const TIME_OPTION = "TimeOption"
export const WEATHER_OPTION = "WeatherOption"

export const MainPannelOpen = bindValue<boolean>(mod.id, MAIN_PANNEL_OPEN, false);
export const CurrentTime = bindValue<number>(mod.id, CURRENT_TIME, 0);
export const CurrentTemperature = bindValue<number>(mod.id, CURRENT_TEMPERATURE, 0);
export const TimeOption = bindValue<number>(mod.id, TIME_OPTION, 0);
export const WeatherOption = bindValue<number>(mod.id, WEATHER_OPTION, 0);


export const SetMainPannelOpen = (open: boolean) => trigger(mod.id, MAIN_PANNEL_OPEN, open);
export const SetCurrentTime = (time: number) => trigger(mod.id, CURRENT_TIME, time);
export const SetCurrentTemperature = (time: number) => trigger(mod.id, CURRENT_TEMPERATURE, time);
export const SetTimeOption = (option: number) => trigger(mod.id, TIME_OPTION, option);
export const SetWeatherOption = (option: number) => trigger(mod.id, WEATHER_OPTION, option);