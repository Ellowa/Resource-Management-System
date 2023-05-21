import { getSession } from "next-auth/react";
import { useState } from "react";
import { RequestPage } from "../components/request-page/RequestPage";
import { ResourcePage } from "../components/resource-page/ResourcePage";
import { SideBar } from "../components/side-bar/SideBar";
import { UserPage } from "../components/user-page/UserPage";
const jwt = require('jsonwebtoken');

export default function Home() {
  const [page, setPage] = useState(2);

  const renderSwitch = () => {
    switch (page) {
      case 1: return <RequestPage />;
      case 2: return <ResourcePage />;
      case 3: return <UserPage />;
    }
  }

  const userDataReciever = async () => {
    const sess = await getSession();
    const payload = jwt.decode(sess.accessToken);
    console.log(payload);
    const userData = {
      name: payload.name,
      role: payload.role,
    }
    console.log(userData);
  }

  return (
    <div className="page">
      <SideBar setPage={setPage} />
      {renderSwitch(page)}
      <button onClick={userDataReciever}>Получить данные пользователя</button>
    </div>
  )
}