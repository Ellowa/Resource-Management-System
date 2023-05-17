import axios from 'axios';
import Router from 'next/router';
import { useState } from 'react';


async function logout() {
    await axios.post('/api/logout')
    Router.reload()
}

export function SideBar(props) {
    const [colorFirst, setColorFirst] = useState(false);
    const [colorSecond, setColorSecond] = useState(true);
    const [colorThird, setColorThird] = useState(false);

    return (
        <div className="side-bar">
            <div className="side-bar__img"></div>
            <p className="side-bar__user-name">Hi, {props.user.user.username}</p>

            {/* <div>
                <button></button>
                <button></button>
            </div> */}

            <div className={colorFirst ? "side-bar__menu side-bar__menu-first side-bar_click" : "side-bar__menu side-bar__menu-first"} onClick={() => { setColorFirst(true); setColorSecond(false); setColorThird(false); props.setPage(1) }}>Запити</div>
            <div className={colorSecond ? "side-bar__menu side-bar_click" : "side-bar__menu"} onClick={() => { setColorFirst(false); setColorSecond(true); setColorThird(false); props.setPage(2) }}>Ресурси</div>
            <div className={colorThird ? "side-bar__menu side-bar_click" : "side-bar__menu"} onClick={() => { setColorFirst(false); setColorSecond(false); setColorThird(true); props.setPage(3) }}>Користувачі</div>


            <div onClick={logout} className="side-bar__log-out">Log Out</div>
        </div>
    );
}