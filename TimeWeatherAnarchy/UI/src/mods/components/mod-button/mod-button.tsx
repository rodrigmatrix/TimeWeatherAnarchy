import { FloatingButton, Button, Tooltip } from "cs2/ui";
import modIcon from "images/mod_icon.svg";
import { SetMainPannelOpen } from "mods/bindings";
import classNames from "classnames";
import { MainPannelOpen } from "mods/bindings";
import { useValue } from "cs2/api";
import styles from "./mod-button-module.scss";

export const ModIconButton = () => {
	const getMainPannelState = useValue(MainPannelOpen);
	return (
		<>
		<Tooltip 
			tooltip="Time and Weather Anarchy">
		   <Button
				variant="floating"
				onClick={() => { SetMainPannelOpen(!getMainPannelState) }}
			>
        		<img style={{ maskImage: `url(${modIcon})` }} />
			</Button>
		</Tooltip>
		</>
	)
}