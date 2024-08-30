import { Button, Tooltip, Icon } from "cs2/ui";
import modIcon from "images/mod_icon.svg";
import { SetMainPanelOpen } from "mods/bindings";
import { MainPannelOpen } from "mods/bindings";
import { useValue } from "cs2/api";

export const ModIconButton = () => {
	const getMainPanelState = useValue(MainPannelOpen);
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