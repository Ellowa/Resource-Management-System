import { useState } from "react";
import { RequestPage } from "../components/request-page/RequestPage";
import { ResourcePage } from "../components/resource-page/ResourcePage";
import { SideBar } from "../components/side-bar/SideBar";
import { withSessionSsr } from "./lib/config/withSession";

export const getServerSideProps = withSessionSsr(
  async ({ req, res }) => {
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
  const [page, setPage] = useState(false);

  return (
    <div className="page">
      <SideBar setPage={setPage} user={user} />
      {page ? <RequestPage></RequestPage> : <ResourcePage></ResourcePage>}

    </div>
  )
}