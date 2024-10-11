import { Button, FOCUS_DISABLED, FocusKey } from "cs2/ui";
import styles from "./text-input.module.scss";
import { Theme } from "cs2/bindings";
import { getModule } from "cs2/modding";
import { useLocalization } from "cs2/l10n";
import { useRef, useState } from "react";
import { VanillaComponentResolver } from "../vanilla-component/vanilla-components";
import arrowLeftClear from "../../../images/RB_ArrowLeftClear.svg";
import classNames from "classnames";

const AssetGridTheme: Theme | any = getModule("game-ui/game/components/asset-menu/asset-grid/asset-grid.module.scss", "classes");

export const TextInputTheme: Theme | any = getModule(
    "game-ui/editor/widgets/item/editor-item.module.scss",
    "classes"
);

export const TextInput = (
    props: {
      onChange?: (val: string) => void; value?: string,
      placeholder : string;
    }
) => {
  const { translate } = useLocalization();
  let [searchQuery, setSearchQuery] = useState<string>(props.value == undefined ? "" : props.value);

  const onChange: React.ChangeEventHandler<HTMLInputElement> = ({ target }) => {
    setSearchQuery(target.value);
    props.onChange && props.onChange(target.value);
  };

  const clearText = () => {
    setSearchQuery("");
    props.onChange && props.onChange("");
  };

  return (
    <div className={styles.container}>
      <div className={styles.searchArea}>
        <input
          value={props.value === undefined ? searchQuery : props.value}
          disabled={false}
          type="text"
          className={classNames(TextInputTheme.input, styles.textBox)}
          onChange={onChange}
        />

        {(props.value === undefined ? searchQuery : props.value) === "" && (
          <span className={styles.placeholder}>{props.placeholder}</span>
        )}

        {searchQuery.trim() !== "" ? (
          <Button
            className={classNames(VanillaComponentResolver.instance.assetGridTheme.item, styles.clearIcon)}
            variant="icon"
            onSelect={clearText}
            focusKey={VanillaComponentResolver.instance.FOCUS_DISABLED}
          >
            <img style={{ maskImage: `url(${arrowLeftClear})` }} />
          </Button>
        ) : null
        }
      </div>
    </div>
  );
};
