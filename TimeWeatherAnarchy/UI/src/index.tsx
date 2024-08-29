import { ModRegistrar } from "cs2/modding";

import { ModIconButton } from "./mods/components/mod-button/mod-button";
import { TimeWeatherPannel } from "mods/pannel/time-weather-pannel";
import {VanillaComponentResolver} from "./mods/components/vanilla-component/vanilla-components";

const register: ModRegistrar = (moduleRegistry) => {
    VanillaComponentResolver.setRegistry(moduleRegistry);
    
    moduleRegistry.append('GameTopRight', ModIconButton);
    moduleRegistry.append("Game", TimeWeatherPannel);
}

export default register;