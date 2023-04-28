import { useState } from 'react';

export function SideBar() {
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

            <div className={colorFirst ? "side-bar__menu side-bar__menu-first side-bar_click" : "side-bar__menu side-bar__menu-first"} onClick={() => { setColorFirst(true); setColorSecond(false) }}>Запити</div>
            <div className={colorSecond ? "side-bar__menu side-bar_click" : "side-bar__menu"} onClick={() => { setColorFirst(false); setColorSecond(true) }}>Ресурси</div>

            <div className="side-bar__log-out">Log Out</div>
        </div>
    );
}