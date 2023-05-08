import { createContext, useContext, useState } from "react";

const Context = createContext();

export function UserProvider({ children }) {
    const [user, setUser] = useState({ name: "test", role: "user" });
    return <Context.Provider value={[user, setUser]}>{children}</Context.Provider>
}

export function useUserContext() {
    return useContext(Context);
}