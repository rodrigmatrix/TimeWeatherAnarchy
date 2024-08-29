import {
    Button,
    Dropdown,
    DropdownItem, DropdownTheme, DropdownToggle,
    FloatingButton, FOCUS_AUTO,
    Panel,
    PanelSection,
    PanelSectionRow,
    Portal,
    Scrollable, Tooltip
} from "cs2/ui";
import modIcon from "images/mod_icon.svg";
import styles from "./time-weather-pannel.module.scss";
import {
    MainPannelOpen,
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
import { CheckBoxWithLine } from "../components/checkbox/checkbox";
const DropdownStyle: Theme | any = getModule("game-ui/menu/themes/dropdown.module.scss", "classes");

export const TimeWeatherPannel = () => {
    const mainPannelOpen = useValue(MainPannelOpen);
    if (!mainPannelOpen) {
        return(<></>)
    }

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
        <>
       <Panel
           header={(
               <div className={styles.header}>
                   <img src={modIcon} className={styles.headerIcon} />
                   <span className={styles.headerText}>Time and Weather Anarchy</span>
               </div>
           )}
           className={styles.panel}
       >
           <Scrollable style={({marginLeft: '8rem', marginRight: '8rem'})}>
               <PanelSection>
                   <h3 className={styles.headerText}>Time Options</h3>
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
                           <h3 className={styles.headerText}>Current Time: {currentTime}</h3>
                           <Slider
                               start={0}
                               end={24}
                               value={currentTime}
                               onChange={value => {
                                   SetCurrentTime(value)
                               }}/>
                           <div style={({marginBottom: '16rem'})}/>
                       </> : null
                   }

                   { showCustomDayOfYear ?
                       <>
                           <div className={styles.headerText}>Custom Day Of The Year: {currentDayOfTheYear}</div>
                           <Slider
                               start={0}
                               end={365}
                               value={currentDayOfTheYear}
                               onChange={value => {
                                   SetCustomDayOfTheYear(value)
                               }}/>
                           <div style={({marginBottom: '16rem'})}/>
                       </> : null
                   }

               </PanelSection>
               <div/>

               <PanelSection>
                   <h3 className={styles.headerText}>Weather Options</h3>

                   <Dropdown
                       theme={DropdownStyle}
                       content={weatherOptions}
                   >
                       <DropdownToggle>
                           {Object.keys(WeatherOptions)[selectedWeatherOption].valueOf()}
                       </DropdownToggle>
                   </Dropdown>

                   {showCustomWeatherTime ?
                       <>
                           <div className={styles.headerText}>Current Weather Date: {currentWeatherTime}</div>
                           <Slider
                               start={0.0}
                               end={1.0}
                               value={currentWeatherTime}
                               onChange={value => {
                                   SetCustomWeatherTime(value)
                               }}/>
                       </> : null
                   }

                   <div style={({marginBottom: '16rem'})}/>

                   <CheckBoxWithLine
                       title={"Enable Custom Temperature"}
                       isChecked={() => enableCustomTemperature}
                       onValueToggle={(value) => {
                           SetEnableCustomTemperature(value)
                       }}/>

                   { enableCustomTemperature ?
                       <>
                           <div className={styles.headerText}>Custom Temperature: {currentTemperature}</div>
                           <Slider
                               start={-50}
                               end={50}
                               value={currentTemperature}
                               onChange={value => { SetCurrentTemperature(value) }}/>
                       </> : null
                   }

                   <div style={({marginBottom: '16rem'})}/>

                   <CheckBoxWithLine
                       title={"Enable Custom Precipitation"}
                       isChecked={() => enableCustomPrecipitation}
                       onValueToggle={(value) => {
                           SetEnableCustomPrecipitation(value)
                       }}/>

                   {enableCustomPrecipitation ?
                       <>
                           <div className={styles.headerText}>Custom Precipitation: {currentPrecipitation}</div>
                           <Slider
                               start={0.0}
                               end={1.0}
                               value={currentPrecipitation}
                               onChange={value => {
                                   SetCustomPrecipitation(value)
                               }}/>
                       </> : null
                   }

                   <div style={({marginBottom: '16rem'})}/>

                   <CheckBoxWithLine
                       title={"Enable Custom Clouds"}
                       isChecked={() => enableCustomClouds}
                       onValueToggle={(value) => {
                           SetEnableCustomClouds(value)
                       }}/>

                   {enableCustomClouds ?
                       <>
                           <div className={styles.headerText}>Custom Clouds: {currentClouds}</div>
                           <Slider
                               start={0.0}
                               end={1.0}
                               value={currentClouds}
                               onChange={value => {
                                   SetCustomClouds(value)
                               }}/>
                       </> : null
                   }

                   <div style={({marginBottom: '16rem'})}/>

                   <CheckBoxWithLine
                       title={"Enable Custom Aurora"}
                       isChecked={() => enableCustomAurora}
                       onValueToggle={(value) => {
                           SetEnableCustomAurora(value)
                       }}/>

                   {enableCustomAurora ?
                       <>
                           <div className={styles.headerText}>Custom Aurora: {currentAurora}</div>
                           <Slider
                               start={0.0}
                               end={1.0}
                               value={currentAurora}
                               onChange={value => {
                                   SetCustomAurora(value)
                               }}/>
                       </> : null
                   }
               </PanelSection>

           </Scrollable>
       </Panel>
        </>
    )
}