import { VanillaComponentResolver} from "../vanilla-component/vanilla-components";

export const Slider = (
    props: {
        start: number,
        end: number,
        value: number;
        onChange: (value: number) => void,
    }
) => {
    return (
        <>
            <VanillaComponentResolver.instance.Slider
                value={props.value}
                start={props.start}
                end={props.end}
                onChange={ value => props.onChange(value)}
            ></VanillaComponentResolver.instance.Slider>
        </>
    );
};