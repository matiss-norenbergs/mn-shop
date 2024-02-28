import { useCallback } from "react"

import Core from "./components/core"
import Input from './components/input'

import Home from './pages/home'
import Login from "./pages/login"
import Users from './pages/users'
import { logoutUser } from "./helpers/axios/userService"

const routes = [
    {
        path: "/",
        title: "Home",
        icon: "home",
        element: Home,
        //menuHidden: true
    },
    {
        path: "/users",
        title: "Users",
        icon: "person",
        element: Users,
        //menuHidden: true
    },
    {
        path: "/login",
        title: "Login",
        icon: "arrow-right-to-bracket",
        element: Login,
        //menuHidden: true
    }
]

const App = () => {

    const handleLogoutUser = useCallback(() => {
        logoutUser()
            .then(response => {
                if (response.status === 200)
                    console.log("success")
            })
    }, [])

    return (
        <Core
            appTitle="Game Shop"
            showHeaderLogo={false}
            routes={routes}
            // extraHeaderContent={<Input
            //     placeholder="Search..."
            // />}
            hideFooter
            logoutUser={handleLogoutUser}
        />
    )
}

export default App