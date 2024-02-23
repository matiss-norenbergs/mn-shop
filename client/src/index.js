import React from 'react'
import ReactDOM from 'react-dom/client'

import Core from 'components/core'

import Home from 'pages/home'
import Users from 'pages/users'

import './index.css'

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

const root = ReactDOM.createRoot(document.getElementById('root'))
root.render(
    <React.StrictMode>
        <Core
            routes={routes}
            hideFooter
        />
    </React.StrictMode>
)