import { useState } from "react";
import { RequestPage } from "../components/request-page/RequestPage";
import { ResourcePage } from "../components/resource-page/ResourcePage";
import { SideBar } from "../components/side-bar/SideBar";
import { UserPage } from "../components/user-page/UserPage";
import { withSessionSsr } from "./lib/config/withSession";

export const getServerSideProps = withSessionSsr(
  async ({ req }) => {
    const user = req.session.user;

    if (!user) {
      return {
        redirect: {
          destination: "/login",
        }
      }
    }

    return {
      props: { user }
    }
  }
)

export default function Home(user) {
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
      <SideBar setPage={setPage} user={user} />
      {renderSwitch(page)}
    </div>
  )
}