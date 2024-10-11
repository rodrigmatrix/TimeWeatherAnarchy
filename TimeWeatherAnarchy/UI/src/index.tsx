import { ModRegistrar } from "cs2/modding";

import { ModIconButton } from "./mods/components/mod-button/mod-button";
import { TimeWeatherPanel } from "./mods/panel/time-weather-panel";
import {VanillaComponentResolver} from "./mods/components/vanilla-component/vanilla-components";
import {RemoveVanillaRightToolbar} from "./mods/components/remove-right-toolbar/remove-right-toolbar";

const register: ModRegistrar = (moduleRegistry) => {
    VanillaComponentResolver.setRegistry(moduleRegistry);
    
    moduleRegistry.append('GameTopRight', ModIconButton);
    moduleRegistry.append("Game", TimeWeatherPanel);
    moduleRegistry.extend("game-ui/game/components/right-menu/right-menu.tsx", "RightMenu", RemoveVanillaRightToolbar);
}

export default register;