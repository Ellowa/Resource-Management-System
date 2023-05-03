import { useState } from "react";
import { RequestPage } from "../components/request-page/RequestPage";
import { ResourcePage } from "../components/resource-page/ResourcePage";
import { SideBar } from "../components/side-bar/SideBar";

export default function Home() {
  const [page, setPage] = useState(false);

  return (
    <div className="page">
      <SideBar setPage={setPage} />
      {page ? <RequestPage></RequestPage> : <ResourcePage></ResourcePage>}

    </div>
  )
}