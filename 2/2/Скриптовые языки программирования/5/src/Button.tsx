import { ReactNode } from "react";

interface IProps{
    title: string;
    callback: () => void;
    disabled?: boolean;
}


export function MyButton(props: IProps){

    return(
        <button id = {props.title} 
        onClick = {props.callback}
        disabled = {(props.disabled == undefined ? false : props.disabled)}>
            {props.title}
        </button>
    );

}