// Developed by Klyte45
import { Component } from "react";
import { Cs2FormLine } from "../form-line/form-line";

export interface Cs2CheckboxProps {
    title: string;
    isChecked: () => boolean;
    onValueToggle: (newVal: boolean) => void;
}

export class CheckBoxWithLine extends Component<Cs2CheckboxProps, {}> {
    constructor(props: Cs2CheckboxProps) {
        super(props);
        this.state = {}
    }
    render() {
        const { title, onValueToggle } = this.props;
        const isChecked = this.props.isChecked();
        return (
            <Cs2FormLine title={title} onClick={() => onValueToggle(!isChecked)}>
                <Cs2Checkbox isChecked={this.props.isChecked} onValueToggle={(x) => onValueToggle(x)} />
            </Cs2FormLine>
        );
    }
}

interface Cs2CheckboxTitleLessProps {
    isChecked: () => boolean;
    onValueToggle: (newVal: boolean) => void;
}

export class Cs2Checkbox extends Component<Cs2CheckboxTitleLessProps, { checked: () => boolean }> {
    constructor(props: Cs2CheckboxProps) {
        super(props);
        this.state = {
            checked: props.isChecked
        }
    }
    render() {
        const { onValueToggle } = this.props;
        const isChecked = this.state.checked();
        return (<><div className={`cs2-toggle cs2-item-mouse-states cs2-toggle2 ${isChecked ? "checked" : "unchecked"}`} onClick={() => onValueToggle(!isChecked)}>
            <div className={`cs2-checkmark ${isChecked ? "checked" : ""}`} ></div>
        </div>
        </>);
    }
}

export interface Cs2TriCheckboxProps {
    isChecked: true | false | null;
    onValueToggle: (newVal: true | false | null) => void;
}

export class CheckBox extends Component<Cs2TriCheckboxProps, {}> {
    constructor(props: Cs2TriCheckboxProps) {
        super(props);
        this.state = {
            checked: props.isChecked
        }
    }
    render() {
        const { onValueToggle, isChecked } = this.props;
        const nextVal: true | false | null = isChecked == true ? null : isChecked !== null;
        return (<><div className={`cs2-toggle cs2-item-mouse-states cs2-toggle2 ${isChecked == true ? "checked" : isChecked === null ? "forbid" : "unchecked"}`} onClick={() => onValueToggle(nextVal)}>
            <div className={`cs2-checkmark ${isChecked == true ? "checked" : isChecked === null ? "forbid" : ""}`} ></div>
        </div>
        </>);
    }
}