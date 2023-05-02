import { useState } from 'react';

export function SideBar(props) {
    const [colorFirst, setColorFirst] = useState(false);
    const [colorSecond, setColorSecond] = useState(true);

    return (
        <div className="side-bar">
            <div className="side-bar__img"></div>
            <p className="side-bar__user-name">Hi, UserName</p>

            {/* <div>
                <button></button>
                <button></button>
            </div> */}

            <div className={colorFirst ? "side-bar__menu side-bar__menu-first side-bar_click" : "side-bar__menu side-bar__menu-first"} onClick={() => { setColorFirst(true); setColorSecond(false); props.setPage(true) }}>Запити</div>
            <div className={colorSecond ? "side-bar__menu side-bar_click" : "side-bar__menu"} onClick={() => { setColorFirst(false); setColorSecond(true); props.setPage(false) }}>Ресурси</div>

            <div className="side-bar__log-out">Log Out</div>
        </div>
    );
}