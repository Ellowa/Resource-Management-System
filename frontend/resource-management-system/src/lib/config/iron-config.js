export const ironOptions = {
    cookieName: 'iron-session',
    password: process.env.COOKIE_SECRET,
    // secure: true should be used in production, can't be used in dev
    cookieOptions: {
        secure: process.env.NODE_ENV === 'production' ? true : false,
    },
}