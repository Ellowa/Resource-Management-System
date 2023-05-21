import { signIn, signOut } from "next-auth/react";

export default function Login() {
    const handleSubmit = async (e) => {
        e.preventDefault();

        const login = e.target.login.value;
        const password = e.target.password.value;

        signIn('credentials', { login: login, password: password })
    };

    return (
        <>
            Temp Login: test_admin
            <form onSubmit={handleSubmit}>
                <label htmlFor="login">Login</label>
                <input type="text" id="login" name="login" required />
                <br />
                <label htmlFor="password">Password</label>
                <input type="password" id="password" name="password" required />
                <br />
                <button type="submit">Sign In</button>
            </form>
            <br />
            <button onClick={() => signOut()}>Sign Out</button>
        </>
    );
}