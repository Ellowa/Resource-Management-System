import { withSessionRoute } from "@/src/lib/config/withSession";

const VALID_EMAIL = "test@gmail.com";
const VALID_PASSWORD = "password";

export default withSessionRoute(createSessionRoute);

async function createSessionRoute(req, res) {
    if (req.method === "POST") {
        const { email, password } = req.body;
        if (email === VALID_EMAIL && password === VALID_PASSWORD) {
            req.session.user = {
                username: "test@gmail.com",
                isAdmin: true
            };
            await req.session.save();
            return res.send({ ok: true });
        }
        return res.status(403).send("");
    }
    return res.status(404).send("");
}

// export default withSessionRoute(createSessionRoute);

// async function createSessionRoute(req, res) {
//     if (req.method === "POST") {
//         const { id } = req.body;
//         const user = GetUserByID(id);
//         if (user.isError) {
//             return res.status(403).send("");
//         }
//         if (!user.isLoading) {
//             req.session.user = {
//                 id: user.user.id,
//                 firstName: user.user.firstName
//             };
//             await req.session.save();
//             res.send({ ok: true });
//         }
//     }
//     return res.status(404).send("");
// }