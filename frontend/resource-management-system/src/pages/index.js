import { useState } from "react";
import { RequestPage } from "../components/request-page/RequestPage";
import { ResourcePage } from "../components/resource-page/ResourcePage";
import { SideBar } from "../components/side-bar/SideBar";
import { UserPage } from "../components/user-page/UserPage";

export default function Home() {
  const [page, setPage] = useState(2);

  const renderSwitch = () => {
    switch (page) {
      case 1: return <RequestPage />;
      case 2: return <ResourcePage />;
      case 3: return <UserPage />;
    }
  }

  return (
    <div className="page">
      <SideBar setPage={setPage} />
      {renderSwitch(page)}
    </div>
  )
}