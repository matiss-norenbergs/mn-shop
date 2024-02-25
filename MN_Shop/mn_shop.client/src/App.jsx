import Core from "./components/core"
import Input from './components/input'

import Home from './pages/home'
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
]

const search = (
    <Input
        placeholder="Search..."
    />
)

const App = () => {
    return (
        <Core
            appTitle="Game Shop"
            showHeaderLogo={false}
            routes={routes}
            extraHeaderContent={search}
            hideFooter
        />
    )
}

export default App