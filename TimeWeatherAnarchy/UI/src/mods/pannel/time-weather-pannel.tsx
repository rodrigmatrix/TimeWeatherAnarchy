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
    CurrentTemperature, SetCurrentTemperature, WeatherOptions, SetWeatherOption, WeatherOption
} from "mods/bindings";
import { bindValue, trigger, useValue } from "cs2/api";
import { TextInputTheme, TextInput } from "../components/text-input/text-input";
import * as React from 'react';
import {getModule} from "cs2/modding";
import {Theme} from "cs2/bindings";
import {Slider} from "../components/slider/slider";
const DropdownStyle: Theme | any = getModule("game-ui/menu/themes/dropdown.module.scss", "classes");

export const TimeWeatherPannel = () => {
    const mainPannelOpen = useValue(MainPannelOpen);
    if (!mainPannelOpen) {
        return(<></>)
    }

    const currentTime = useValue(CurrentTime);
    const currentTemperature = useValue(CurrentTemperature);
    const selectedTimeOption = useValue(TimeOption);
    const selectedWeatherOption = useValue(WeatherOption);
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
           <Scrollable>
               <PanelSection>
                   <h3 className={styles.headerText}>Current Time: {currentTime}</h3>
                   {/*<div className={styles.mode}>*/}
                   {/*    <div*/}
                   {/*        onClick={() => {*/}

                   {/*        }}*/}
                   {/*    >*/}
                   {/*        Day*/}
                   {/*    </div>*/}
                   {/*</div>*/}

                   <Dropdown
                       theme={DropdownStyle}
                       content={timeOptions}
                   >
                       <DropdownToggle>
                           {Object.keys(TimeOptions)[selectedTimeOption].valueOf()}
                       </DropdownToggle>
                   </Dropdown>

                   <Slider
                       start={0}
                       end={24}
                       value={currentTime}
                       onChange={ value => { SetCurrentTime(value) } }/>

               </PanelSection>

               <PanelSection>
                    <h3 className={styles.headerText}>Current Temperature: {currentTemperature}</h3>

                   <Dropdown
                       theme={DropdownStyle}
                       content={weatherOptions}
                   >
                       <DropdownToggle>
                           {Object.keys(WeatherOptions)[selectedWeatherOption].valueOf()}
                       </DropdownToggle>
                   </Dropdown>

                   <Slider
                       start={-50}
                       end={50}
                       value={currentTemperature}
                       onChange={ value => { SetCurrentTemperature(value) } }/>
                </PanelSection>
            </Scrollable>
       </Panel>
	    </>
    )
}

export default TimeWeatherPannel;