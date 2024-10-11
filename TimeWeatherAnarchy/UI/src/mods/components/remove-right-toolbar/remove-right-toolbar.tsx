import { useValue } from "cs2/api";
import { ModuleRegistryExtend } from "cs2/modding";
import {MainPanelOpen} from "../../bindings";

export const RemoveVanillaRightToolbar: ModuleRegistryExtend = (Component) => {
    return (props) => {
        const { children, ...otherProps } = props || {};
        const mainPanelOpen = useValue(MainPanelOpen);

        if (mainPanelOpen) {
            return <></>;
        }

        return <Component {...otherProps}>{children}</Component>;
    };
};