import { useState } from "react";
import { ResourcePage } from "../components/resource-page/ResourcePage";
import { SideBar } from "../components/side-bar/SideBar";
import { RequestPage } from "../components/request-page/RequestPage";

export default function Home() {
  const [page, setPage] = useState(false);

  return (
    <div className="page">
      <SideBar setPage = {setPage}/>
      {page ? <RequestPage></RequestPage> : <ResourcePage></ResourcePage>}

    </div>
  )
}