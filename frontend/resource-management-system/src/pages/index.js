import { MainPage } from "../components/main-page/MainPage";
import { SideBar } from "../components/side-bar/SideBar";

export default function Home() {
  return (
    <div className="page">
      <SideBar />
      <MainPage />
    </div>
  )
}