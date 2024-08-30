import { ModRegistrar } from "cs2/modding";

import { ModIconButton } from "./mods/components/mod-button/mod-button";
import { TimeWeatherPanel } from "./mods/panel/time-weather-panel";
import {VanillaComponentResolver} from "./mods/components/vanilla-component/vanilla-components";

const register: ModRegistrar = (moduleRegistry) => {
    VanillaComponentResolver.setRegistry(moduleRegistry);
    
    moduleRegistry.append('GameTopRight', ModIconButton);
    moduleRegistry.append("Game", TimeWeatherPanel);
}

export default register;