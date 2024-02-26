import Core from "./components/core"
import Input from './components/input'

import Home from './pages/home'
import Login from "./pages/login"
import Users from './pages/users'

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
    return (
        <Core
            appTitle="Game Shop"
            showHeaderLogo={false}
            routes={routes}
            // extraHeaderContent={<Input
            //     placeholder="Search..."
            // />}
            hideFooter
        />
    )
}

export default App