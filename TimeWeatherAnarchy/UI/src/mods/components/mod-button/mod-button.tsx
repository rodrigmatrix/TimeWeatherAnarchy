import { Button, Tooltip, Icon } from "cs2/ui";
import modIcon from "images/mod_icon.svg";
import { SetMainPanelOpen } from "mods/bindings";
import { MainPanelOpen } from "mods/bindings";
import { useValue } from "cs2/api";

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
					tinted={true}
					src={modIcon}/>
			</Button>
		</Tooltip>
		</>
	)
}