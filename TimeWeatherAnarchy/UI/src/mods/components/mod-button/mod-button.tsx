import { Button, Tooltip, Icon } from "cs2/ui";
import modIcon from "images/mod-icon.png";
import { SetMainPanelOpen } from "mods/bindings";
import { MainPanelOpen } from "mods/bindings";
import { useValue } from "cs2/api";
import styles from "./mod-button.module.scss"

export const ModIconButton = () => {
	const getMainPanelState = useValue(MainPanelOpen);
	return (
		<>
		<Tooltip
			tooltip="Time and Weather Anarchy">
		   <Button
				variant="floating"
				onClick={() => { SetMainPanelOpen(!getMainPanelState) }}>
        		<Icon
					tinted={false}
					className={styles.icon}
					src={modIcon}/>
			</Button>
		</Tooltip>
		</>
	)
}