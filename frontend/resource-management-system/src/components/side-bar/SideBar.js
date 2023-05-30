import { signOut, useSession } from 'next-auth/react';
import { useState } from 'react';
const jwt = require('jsonwebtoken');

async function logout() {
    await signOut();
}

function UserInfo() {
    const session = useSession();
    if (session.status === 'authenticated') {
        const payload = jwt.decode(session.data.accessToken);
        const userData = {
            name: payload.name,
            role: payload.role,
        }
        return userData;
    }
}


function UserName() {
    const userData = UserInfo();
    if (userData?.name === undefined) return <div>Loading...</div>
    return <p className="side-bar__user-name">Hi, {userData.name}</p>
}

function SideBarButtons(props) {
    props = props.props
    const [colorFirst, setColorFirst] = useState(false);
    const [colorSecond, setColorSecond] = useState(true);
    const [colorThird, setColorThird] = useState(false);

    const userData = UserInfo();
    if (userData?.role === "admin") {
        return (
            <div className={colorThird ? "side-bar__menu side-bar_click" : "side-bar__menu"} onClick={() => { setColorFirst(false); setColorSecond(false); setColorThird(true); props.setPage(3) }}>Користувачі</div>
        )
    }
    if (userData?.role === "manager") {
        return (
            <>
                <div className={colorFirst ? "side-bar__menu side-bar__menu-first side-bar_click" : "side-bar__menu side-bar__menu-first"} onClick={() => { setColorFirst(true); setColorSecond(false); setColorThird(false); props.setPage(1) }}>Запити</div>
                <div className={colorSecond ? "side-bar__menu side-bar_click" : "side-bar__menu"} onClick={() => { setColorFirst(false); setColorSecond(true); setColorThird(false); props.setPage(2) }}>Ресурси</div>
            </>
        )
    }
    if (userData?.role === "user") {
        return (
            <div className={colorFirst ? "side-bar__menu side-bar__menu-first side-bar_click" : "side-bar__menu side-bar__menu-first"} onClick={() => { setColorFirst(true); setColorSecond(false); setColorThird(false); props.setPage(1) }}>Запити</div>
        )
    }
    return (<></>)
}

export function SideBar(props) {

    return (
        <div className="side-bar">
            <div className="side-bar__img"></div>
            <UserName />
            <SideBarButtons props={props} />

            {/* <div className={colorFirst ? "side-bar__menu side-bar__menu-first side-bar_click" : "side-bar__menu side-bar__menu-first"} onClick={() => { setColorFirst(true); setColorSecond(false); setColorThird(false); props.setPage(1) }}>Запити</div>
            <div className={colorSecond ? "side-bar__menu side-bar_click" : "side-bar__menu"} onClick={() => { setColorFirst(false); setColorSecond(true); setColorThird(false); props.setPage(2) }}>Ресурси</div>
            <div className={colorThird ? "side-bar__menu side-bar_click" : "side-bar__menu"} onClick={() => { setColorFirst(false); setColorSecond(false); setColorThird(true); props.setPage(3) }}>Користувачі</div> */}

            <div onClick={logout} className="side-bar__log-out">Log Out</div>
        </div>
    );
}