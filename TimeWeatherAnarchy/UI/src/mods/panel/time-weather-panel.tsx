import {
    Button,
    Dropdown,
    DropdownItem, DropdownTheme, DropdownToggle,
    FloatingButton, FOCUS_AUTO, Icon,
    Panel,
    PanelSection,
    PanelSectionRow,
    Portal,
    Scrollable, Tooltip
} from "cs2/ui";
import modIcon from "images/mod_icon.svg";
import styles from "./time-weather-panel.module.scss";
import {
    MainPanelOpen,
    CurrentTime,
    SetCurrentTime,
    TimeOption,
    TimeOptions,
    SetTimeOption,
    CurrentTemperature,
    SetCurrentTemperature,
    WeatherOptions,
    SetWeatherOption,
    WeatherOption,
    EnableCustomPrecipitation,
    SetCustomPrecipitation,
    SetEnableCustomPrecipitation,
    SetEnableCustomAurora,
    EnableCustomAurora,
    EnableCustomClouds,
    CurrentPrecipitation,
    CurrentClouds,
    CurrentAurora,
    CurrentDayOfTheYear,
    SetCustomClouds,
    SetEnableCustomClouds,
    SetCustomAurora,
    SetCustomDayOfTheYear,
    EnableCustomTemperature,
    CurrentWeatherTime, SetCustomWeatherTime, SetEnableCustomTemperature
} from "mods/bindings";
import { useValue } from "cs2/api";
import * as React from 'react';
import { getModule } from "cs2/modding";
import { Theme } from "cs2/bindings";
import { Slider } from "../components/slider/slider";
import {CheckBoxWithLine, Cs2Checkbox} from "../components/checkbox/checkbox";
import { Section } from "../components/section/section";
const DropdownStyle: Theme | any = getModule("game-ui/menu/themes/dropdown.module.scss", "classes");

export const TimeWeatherPanel = () => {
    const mainPannelOpen = useValue(MainPanelOpen);
    const currentTime = useValue(CurrentTime);
    const currentTemperature = useValue(CurrentTemperature);
    const currentDayOfTheYear = useValue(CurrentDayOfTheYear);
    const currentPrecipitation = useValue(CurrentPrecipitation);
    const currentWeatherTime = useValue(CurrentWeatherTime);
    const currentClouds = useValue(CurrentClouds);
    const currentAurora = useValue(CurrentAurora);
    const selectedTimeOption = useValue(TimeOption);
    const selectedWeatherOption = useValue(WeatherOption);
    const enableCustomPrecipitation = useValue(EnableCustomPrecipitation);
    const enableCustomClouds = useValue(EnableCustomClouds);
    const enableCustomAurora = useValue(EnableCustomAurora);
    const enableCustomTemperature = useValue(EnableCustomTemperature);
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
                {key}
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
                {key}
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
                   <span className={styles.headerText}>Time and Weather Anarchy</span>
               </div>
           )}
           className={styles.panel}
       >
           <Scrollable>
               <Section>
                   <span className={styles.optionHeader}>Time Options</span>
                   <Dropdown
                       theme={DropdownStyle}
                       content={timeOptions}
                   >
                       <DropdownToggle>
                           {Object.keys(TimeOptions)[selectedTimeOption].valueOf()}
                       </DropdownToggle>
                   </Dropdown>
                   <div style={({marginBottom: '16rem'})}/>

                   { showCustomTime ?
                       <>
                           <Slider
                               start={0}
                               end={24}
                               value={currentTime}
                               onChange={value => {
                                   SetCurrentTime(value)
                               }}/>
                           <span className={styles.sliderText}>Hour of the day: {currentTime}</span>
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
                           <span className={styles.sliderText}>Day of the year: {currentDayOfTheYear}</span>

                       </div> : null
                   }

               </Section>

               <Section>
                   <span className={styles.optionHeader}>Weather Options</span>

                   <Dropdown
                       theme={DropdownStyle}
                       content={weatherOptions}
                   >
                       <DropdownToggle>
                           {Object.keys(WeatherOptions)[selectedWeatherOption].valueOf()}
                       </DropdownToggle>
                   </Dropdown>

                   { showCustomWeatherTime ?
                       <>
                           <div style={({marginBottom: '16rem'})}/>
                           <Slider
                               start={0.0}
                               end={1.0}
                               value={currentWeatherTime}
                               onChange={value => {
                                   SetCustomWeatherTime(value)
                               }}/>
                           <span className={styles.sliderText}>Weather Date: {currentWeatherTime.toFixed(3)}</span>
                       </> : null
                   }

                   <div style={({marginBottom: '16rem'})}/>

                   <div className={styles.container}>
                       <CheckBoxWithLine
                           title={"Enable Custom Temperature"}
                           isChecked={enableCustomTemperature}
                           onValueToggle={(value) => {
                               SetEnableCustomTemperature(value)
                           }}/>

                       { enableCustomTemperature ?
                           <>
                               <div style={({marginBottom: '16rem'})}/>
                               <Slider
                                   start={-50}
                                   end={50}
                                   value={currentTemperature}
                                   onChange={value => {
                                       SetCurrentTemperature(value)
                                   }}/>
                               <span className={styles.sliderText}>Temperature: {currentTemperature}</span>
                           </> : null
                       }
                   </div>

                   <div style={({marginBottom: '16rem'})}/>

                   <div className={styles.container}>
                       <CheckBoxWithLine
                           title={"Enable Custom Precipitation"}
                           isChecked={enableCustomPrecipitation}
                           onValueToggle={(value) => {
                               SetEnableCustomPrecipitation(value)
                           }}/>

                       { enableCustomPrecipitation ?
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
                                   className={styles.sliderText}>Precipitation: {currentPrecipitation.toFixed(3)}</span>
                           </> : null
                       }
                   </div>

                   <div style={({marginBottom: '16rem'})}/>

                   <div className={styles.container}>
                       <CheckBoxWithLine
                           title={"Enable Custom Clouds"}
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
                               <span className={styles.sliderText}>Clouds: {currentClouds.toFixed(3)}</span>
                           </> : null
                       }
                   </div>

                   <div style={({marginBottom: '16rem'})}/>

                   <div className={styles.container}>
                       <CheckBoxWithLine
                           title={"Enable Custom Aurora"}
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
                               <span className={styles.sliderText}>Aurora: {currentAurora.toFixed(3)}</span>
                           </> : null
                       }
                   </div>
               </Section>
           </Scrollable>
       </Panel>
        </div>
    )
}