import React from "react";

export default function Modal(props){
    return(
        <div className="modal">
            <div className="modal__container">
                <button className="modal__button" onClick={() => props.closeModal(false)}> X </button>
                <div className="modal_body">
                <props.funcName user = {props.user}
                                resource = {props.user}></props.funcName>
                </div>
            </div>
        </div>
    )
}
