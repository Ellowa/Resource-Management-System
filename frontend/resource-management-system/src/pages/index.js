import { ResourcePage } from "../components/resource-page/ResourcePage";
import { SideBar } from "../components/side-bar/SideBar";

export default function Home() {
  return (
    <div className="page">
      <SideBar />
      {/* TODO: If button on resources - Resource Page, if on requests - RequestPage, if on any other button - corresponding page */}
      <ResourcePage />
      {/* <RequestPage /> */}
    </div>
  )
}