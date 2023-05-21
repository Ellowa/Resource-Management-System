// import '@/styles/globals.css'
import { Analytics } from '@vercel/analytics/react'
import { SessionProvider } from 'next-auth/react'
import { useState } from 'react'
import RefreshTokenHandler from '../components/refreshTokenHandler'
import './index.css'

export default function App({ Component, pageProps }) {
  const [interval, setInterval] = useState(0);
  return (
    <SessionProvider session={pageProps.session} refetchInterval={interval}>
      <Component {...pageProps} />
      <Analytics />
      <RefreshTokenHandler setInterval={setInterval} />
    </SessionProvider>
  )
}
