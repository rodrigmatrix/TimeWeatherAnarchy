import {Dropdown, DropdownItem, DropdownToggle, FOCUS_AUTO, Icon, Panel, Scrollable} from "cs2/ui";
import modIcon from "images/mod_icon.svg";
import styles from "./time-weather-panel.module.scss";
import {
    CurrentAurora,
    CurrentClouds,
    CurrentDayOfTheYear,
    CurrentPrecipitation,
    CurrentTemperature,
    CurrentTime,
    CurrentWeatherTime, CustomFog, CustomRainbow, CustomThunder,
    EnableCustomAurora,
    EnableCustomClouds, EnableCustomFog,
    EnableCustomPrecipitation,
    EnableCustomTemperature, EnableCustomThunder,
    MainPanelOpen,
    SetCurrentTemperature,
    SetCurrentTime,
    SetCustomAurora,
    SetCustomClouds,
    SetCustomDayOfTheYear, SetCustomFog,
    SetCustomPrecipitation, SetCustomRainbow, SetCustomThunder,
    SetCustomWeatherTime,
    SetEnableCustomAurora,
    SetEnableCustomClouds, SetEnableCustomFog,
    SetEnableCustomPrecipitation,
    SetEnableCustomTemperature, SetEnableCustomThunder,
    SetTimeOption,
    SetWeatherOption,
    TimeOption,
    TimeOptions,
    WeatherOption,
    WeatherOptions
} from "mods/bindings";
import {useValue} from "cs2/api";
import * as React from 'react';
import {getModule} from "cs2/modding";
import {Theme} from "cs2/bindings";
import {Slider} from "../components/slider/slider";
import {CheckBoxWithLine} from "../components/checkbox/checkbox";
import {Section} from "../components/section/section";
import {useLocalization} from "cs2/l10n";
import {FormLine} from "../components/form-line/form-line";

const DropdownStyle: Theme | any = getModule("game-ui/menu/themes/dropdown.module.scss", "classes");

const convertNumToTime = (number: number): string => {
    let sign = (number >= 0) ? 1 : -1;
    number = number * sign;
    let hour = Math.floor(number);
    let decPart = number - hour;
    let min = 1 / 60;
    decPart = min * Math.round(decPart / min);
    let minute = Math.floor(decPart * 60) + '';
    if (minute.length < 2) {
        minute = '0' + minute;
    }
    return (sign == 1 ? '' : '-') + hour + ':' + minute;
}

export const TimeWeatherPanel = () => {
    const { translate } = useLocalization();
    const mainPannelOpen = useValue(MainPanelOpen);
    const currentTime = useValue(CurrentTime);
    const currentTemperature = useValue(CurrentTemperature);
    const currentDayOfTheYear = useValue(CurrentDayOfTheYear);
    const currentPrecipitation = useValue(CurrentPrecipitation);
    const customFog = useValue(CustomFog);
    const customThunder = useValue(CustomThunder);
    const customRainbow = useValue(CustomRainbow);
    const currentWeatherTime = useValue(CurrentWeatherTime);
    const currentClouds = useValue(CurrentClouds);
    const currentAurora = useValue(CurrentAurora);
    const selectedTimeOption = useValue(TimeOption);
    const selectedWeatherOption = useValue(WeatherOption);
    const enableCustomPrecipitation = useValue(EnableCustomPrecipitation);
    const enableCustomClouds = useValue(EnableCustomClouds);
    const enableCustomAurora = useValue(EnableCustomAurora);
    const enableCustomTemperature = useValue(EnableCustomTemperature);
    const enableCustomFog = useValue(EnableCustomFog);
    const enableCustomThunder = useValue(EnableCustomThunder);
    const showCustomTime = selectedTimeOption == TimeOptions.Custom
    const showCustomDayOfYear = selectedTimeOption != TimeOptions.Default
    const showCustomWeatherTime = selectedWeatherOption == WeatherOptions.Custom

    if (!mainPannelOpen) {
        return(<></>)
    }

    const timeOptions = Object.keys(TimeOptions)
        .map((key, value) => (
            <DropdownItem
                theme={DropdownStyle}
                focusKey={FOCUS_AUTO}
                value={value}
                closeOnSelect={true}
                onChange={() => { SetTimeOption(value) }}
            >
                {translate("TimeWeatherAnarchy." + key)}
            </DropdownItem>
        ))
    const weatherOptions = Object.keys(WeatherOptions)
        .map((key, value) => (
            <DropdownItem
                theme={DropdownStyle}
                focusKey={FOCUS_AUTO}
                value={value}
                closeOnSelect={true}
                onChange={() => { SetWeatherOption(value) }}
            >
                {translate("TimeWeatherAnarchy." + key)}
            </DropdownItem>
        ))

    return (
        <div>
       <Panel
           header={(
               <div className={styles.header}>
                   <Icon
                       tinted={true}
                       src={modIcon}
                       className={styles.headerIcon}/>
                   <span className={styles.headerText}>{translate("TimeWeatherAnarchy.ModName")}</span>
               </div>
           )}
           className={styles.panel}
       >
           <Scrollable>
               <Section>
                   <span className={styles.optionHeader}>{translate("TimeWeatherAnarchy.TimeOptions")}</span>
                   <Dropdown
                       theme={DropdownStyle}
                       content={timeOptions}
                   >
                       <DropdownToggle>
                           {translate("TimeWeatherAnarchy." + Object.keys(TimeOptions)[selectedTimeOption].valueOf())}
                       </DropdownToggle>
                   </Dropdown>
                   <div style={({marginBottom: '16rem'})}/>

                   { showCustomTime ?
                       <>
                           <Slider
                               start={0.0}
                               end={23.99}
                               value={currentTime}
                               onChange={value => {
                                   SetCurrentTime(value)
                               }}/>
                           <span className={styles.sliderText}>{translate("TimeWeatherAnarchy.HourOfTheDay")}: {convertNumToTime(currentTime)}</span>
                           <div style={({marginBottom: '16rem'})}/>
                       </> : null
                   }

                   { showCustomDayOfYear ?
                       <div>
                           <Slider
                               start={0}
                               end={365}
                               value={currentDayOfTheYear}
                               onChange={value => {
                                   SetCustomDayOfTheYear(value)
                               }}/>
                           <span className={styles.sliderText}>{translate("TimeWeatherAnarchy.DayOfTheYear")}: {currentDayOfTheYear}</span>

                       </div> : null
                   }

               </Section>

               <Section>
                   <span className={styles.optionHeader}>{translate("TimeWeatherAnarchy.WeatherOptions")}</span>

                   <Dropdown
                       theme={DropdownStyle}
                       content={weatherOptions}
                   >
                       <DropdownToggle>
                           {translate("TimeWeatherAnarchy." + Object.keys(WeatherOptions)[selectedWeatherOption].valueOf())}
                       </DropdownToggle>
                   </Dropdown>

                   {showCustomWeatherTime ?
                       <>
                           <div style={({marginBottom: '16rem'})}/>
                           <Slider
                               start={0.0}
                               end={1.0}
                               value={currentWeatherTime}
                               onChange={value => {
                                   SetCustomWeatherTime(value)
                               }}/>
                           <span
                               className={styles.sliderText}>{translate("TimeWeatherAnarchy.WeatherDate")}: {currentWeatherTime.toFixed(3)}</span>
                       </> : null
                   }

                   <div style={({marginBottom: '16rem'})}/>

                   <div className={styles.container}>
                       <CheckBoxWithLine
                           title={translate("TimeWeatherAnarchy.EnableCustomTemperature")}
                           isChecked={enableCustomTemperature}
                           onValueToggle={(value) => {
                               SetEnableCustomTemperature(value)
                           }}/>

                       {enableCustomTemperature ?
                           <>
                               <div style={({marginBottom: '16rem'})}/>
                               <Slider
                                   start={-50}
                                   end={50}
                                   value={currentTemperature}
                                   onChange={value => {
                                       SetCurrentTemperature(value)
                                   }}/>
                               <span
                                   className={styles.sliderText}>{translate("TimeWeatherAnarchy.Temperature")}: {currentTemperature}</span>
                           </> : null
                       }
                   </div>

                   <div style={({marginBottom: '16rem'})}/>

                   <div className={styles.container}>
                       <CheckBoxWithLine
                           title={translate("TimeWeatherAnarchy.EnableCustomPrecipitation")}
                           isChecked={enableCustomPrecipitation}
                           onValueToggle={(value) => {
                               SetEnableCustomPrecipitation(value)
                           }}/>

                       {enableCustomPrecipitation ?
                           <>
                               <div style={({marginBottom: '16rem'})}/>
                               <Slider
                                   start={0.0}
                                   end={1.0}
                                   value={currentPrecipitation}
                                   onChange={value => {
                                       SetCustomPrecipitation(value)
                                   }}/>
                               <span
                                   className={styles.sliderText}>{translate("TimeWeatherAnarchy.Precipitation")}: {currentPrecipitation.toFixed(3)}</span>
                           </> : null
                       }
                   </div>

                   <div style={({marginBottom: '16rem'})}/>

                   {/*<div className={styles.container}>*/}
                   {/*    <CheckBoxWithLine*/}
                   {/*        title={translate("TimeWeatherAnarchy.EnableCustomThunder")}*/}
                   {/*        isChecked={enableCustomThunder}*/}
                   {/*        onValueToggle={(value) => {*/}
                   {/*            SetEnableCustomThunder(value)*/}
                   {/*        }}/>*/}

                   {/*    {enableCustomThunder ?*/}
                   {/*        <>*/}
                   {/*            <div style={({marginBottom: '16rem'})}/>*/}
                   {/*            <Slider*/}
                   {/*                start={0.0}*/}
                   {/*                end={1.0}*/}
                   {/*                value={customThunder}*/}
                   {/*                onChange={value => {*/}
                   {/*                    SetCustomThunder(value)*/}
                   {/*                }}/>*/}
                   {/*            <div style={({marginBottom: '4rem'})}/>*/}
                   {/*            <span*/}
                   {/*                className={styles.sliderText}>{translate("TimeWeatherAnarchy.Thunder")}: {customThunder.toFixed(3)}</span>*/}
                   {/*        </> : null*/}
                   {/*    }*/}
                   {/*</div>*/}

                   {/*<div style={({marginBottom: '16rem'})}/>*/}

                   <div className={styles.container}>
                       <CheckBoxWithLine
                           title={translate("TimeWeatherAnarchy.EnableCustomClouds")}
                           isChecked={enableCustomClouds}
                           onValueToggle={(value) => {
                               SetEnableCustomClouds(value)
                           }}/>

                       {enableCustomClouds ?
                           <>
                               <div style={({marginBottom: '16rem'})}/>
                               <Slider
                                   start={0.0}
                                   end={1.0}
                                   value={currentClouds}
                                   onChange={value => {
                                       SetCustomClouds(value)
                                   }}/>
                               <span
                                   className={styles.sliderText}>{translate("TimeWeatherAnarchy.Clouds")}: {currentClouds.toFixed(3)}</span>
                           </> : null
                       }
                   </div>

                   <div style={({marginBottom: '16rem'})}/>

                   <div className={styles.container}>
                       <CheckBoxWithLine
                           title={translate("TimeWeatherAnarchy.EnableCustomAurora")}
                           isChecked={enableCustomAurora}
                           onValueToggle={(value) => {
                               SetEnableCustomAurora(value)
                           }}/>

                       {enableCustomAurora ?
                           <>
                               <div style={({marginBottom: '16rem'})}/>
                               <Slider
                                   start={0.0}
                                   end={1.0}
                                   value={currentAurora}
                                   onChange={value => {
                                       SetCustomAurora(value)
                                   }}/>
                               <div style={({marginBottom: '4rem'})}/>
                               <span
                                   className={styles.sliderText}>{translate("TimeWeatherAnarchy.Aurora")}: {currentAurora.toFixed(3)}</span>
                           </> : null
                       }
                   </div>

                   <div style={({marginBottom: '16rem'})}/>

                   {/*<div className={styles.container}>*/}
                   {/*    <CheckBoxWithLine*/}
                   {/*        title={translate("TimeWeatherAnarchy.EnableCustomFog")}*/}
                   {/*        isChecked={enableCustomFog}*/}
                   {/*        onValueToggle={(value) => {*/}
                   {/*            SetEnableCustomFog(value)*/}
                   {/*        }}/>*/}

                   {/*    {enableCustomFog ?*/}
                   {/*        <>*/}
                   {/*            <div style={({marginBottom: '16rem'})}/>*/}
                   {/*            <Slider*/}
                   {/*                start={0.0}*/}
                   {/*                end={1.0}*/}
                   {/*                value={customFog}*/}
                   {/*                onChange={value => {*/}
                   {/*                    SetCustomFog(value)*/}
                   {/*                }}/>*/}
                   {/*            <div style={({marginBottom: '4rem'})}/>*/}
                   {/*            <span*/}
                   {/*                className={styles.sliderText}>{translate("TimeWeatherAnarchy.Fog")}: {customFog.toFixed(3)}</span>*/}
                   {/*        </> : null*/}
                   {/*    }*/}
                   {/*</div>*/}

                   {/*<div style={({marginBottom: '16rem'})}/>*/}

                   {/*<div className={styles.container}>*/}
                   {/*    <FormLine title={translate("TimeWeatherAnarchy.RainbowStrength")}/>*/}
                   {/*    <div style={({marginBottom: '16rem'})}/>*/}
                   {/*    <Slider*/}
                   {/*        start={0.0}*/}
                   {/*        end={1.0}*/}
                   {/*        value={customRainbow}*/}
                   {/*        onChange={value => {*/}
                   {/*            SetCustomRainbow(value)*/}
                   {/*        }}/>*/}
                   {/*    <div style={({marginBottom: '4rem'})}/>*/}
                   {/*    <span*/}
                   {/*        className={styles.sliderText}>{translate("TimeWeatherAnarchy.Rainbow")}: {customRainbow.toFixed(3)}</span>*/}
                   {/*</div>*/}

               </Section>
           </Scrollable>
       </Panel>
        </div>
    )
}