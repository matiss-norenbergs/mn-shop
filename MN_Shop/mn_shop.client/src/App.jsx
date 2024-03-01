import { Provider } from "react-redux"

import Core from "./components/core"
import Input from './components/input'

import Home from './pages/home'
import Login from "./pages/login"
import Users from './pages/users'
import Products from "./pages/products"

import store from "./redux/stores/store"

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
        //menuHidden: true,
        admin: true
    },
    {
        path: "/products",
        title: "Products",
        icon: "warehouse",
        element: Products,
        admin: true
    },
    {
        path: "/login",
        title: "Login",
        icon: "arrow-right-to-bracket",
        element: Login,
        menuHidden: true
    }
]

const App = () => {
    return (
        <Provider store={store}>
            <Core
                appTitle="Game Shop"
                showHeaderLogo={false}
                routes={routes}
                // extraHeaderContent={<Input
                //     placeholder="Search..."
                // />}
                hideFooter
            />
        </Provider>
    )
}

export default App