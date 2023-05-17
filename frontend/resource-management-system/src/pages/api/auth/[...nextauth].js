import axios from "axios";
import NextAuth from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";
const jwt = require('jsonwebtoken');

async function refreshAccesToken(tokenObject) {
    try {
        const tokenResponse = await axios.post("https://resource-ms-backend.azurewebsites.net/api/auth/refresh", {
            token: tokenObject.refreshToken,
        });

        return {
            ...tokenObject,
            accessToken: tokenResponse.data.accessToken,
            accessTokenExpiry: tokenResponse.data.accessTokenExpiry,
            refreshToken: tokenResponse.data.refreshToken,
        }
    } catch (error) {
        return {
            ...tokenObject,
            error: "RefreshAccessTokenError",
        }
    }
}

const providers = [
    CredentialsProvider({
        name: 'Credentials',
        authorize: async (credentials) => {
            try {
                const user = await axios.post('https://resource-ms-backend.azurewebsites.net/api/user/login', {
                    login: credentials.login,
                    password: credentials.password
                });

                if (user.data.accessToken) {
                    return user.data;
                }

                return null;
            } catch (e) {
                throw new Error(e);
            }
        }
    })
]

const callbacks = {
    jwt: async ({ token, user }) => {
        if (user) {
            token.accessToken = user.accessToken;
            const decodedToken = jwt.decode(user.accessToken);
            token.accessTokenExpiry = decodedToken.exp;
            token.refreshToken = user.refreshToken;
        }

        console.log(token);

        const shouldRefreshTime = Math.round((token.accessTokenExpiry - 60 * 60 * 1000) - Date.now())

        if (shouldRefreshTime > 0) {
            return Promise.resolve(token);
        }

        token = refreshAccesToken(token);
        return Promise.resolve(token);
    },
    session: async ({ session, token }) => {
        session.accessToken = token.accessToken;
        session.accessTokenExpiry = token.accessTokenExpiry;
        session.error = token.error;

        return Promise.resolve(session);
    },
}

export const options = {
    providers,
    callbacks,
    pages: {},
    secret: process.env.NEXTAUTH_SECRET,
}

const Auth = (req, res) => NextAuth(req, res, options);
export default Auth;