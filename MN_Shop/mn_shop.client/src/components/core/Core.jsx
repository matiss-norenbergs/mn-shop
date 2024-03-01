import PropTypes from "prop-types"
import { useCallback, useEffect, useMemo } from "react"
import { useDispatch, useSelector } from "react-redux"
import { BrowserRouter as Router, Routes, Route } from "react-router-dom"

import { library } from "@fortawesome/fontawesome-svg-core"
import { far } from "@fortawesome/free-regular-svg-icons"
import { fas } from "@fortawesome/free-solid-svg-icons"

import Header from "../header"
import Footer from "../footer"

import { ProtectedRoute } from "./components/Protected"

import { updateUserData } from "@/helpers/axios/authService"
import { setUser } from "@/redux/features/user/userSlice"

import styles from "./Core.module.css"

library.add(far, fas)

const propTypes = {
    routes: PropTypes.array,
    hideFooter: PropTypes.bool,
    extraHeaderContent: PropTypes.node,
    appTitle: PropTypes.string,
    showHeaderLogo: PropTypes.bool
}
const defaultProps = {
    routes: []
}

const Core = ({
    routes,
    hideFooter,
    extraHeaderContent,
    appTitle,
    showHeaderLogo
}) => {
    const user = useSelector((state) => state.user)
    const dispatch = useDispatch()

    const headerPaths = useMemo(() => {
        return routes.filter(({ menuHidden, admin }) => !menuHidden && (!admin || (admin && user?.IsAdmin))).map(({ path, title, icon }) => ({
            path,
            title,
            icon
        }))
    }, [routes, user])

    const handleGetUser = useCallback(() => {
        updateUserData()
            .then(response => {
                if (response.status === 200) {
                    dispatch(setUser(response.data))
                }
            })
            .catch(() => {
                console.log("NO USER LOGGED IN")
            })
    }, [dispatch])

    const handleRoutes = useMemo(() => {
        const protectedRoutes = []
        const renderRoutes = []

        routes.forEach(({ path, element: Element, admin }) => {
            if (admin) {
                protectedRoutes.push(<Route
                    key={path}
                    exact={path === "/"}
                    path={path}
                    element={<Element />}
                />)
            } else {
                renderRoutes.push(<Route
                    key={path}
                    exact={path === "/"}
                    path={path}
                    element={<Element />}
                />)
            }
        })

        return [
            ...renderRoutes,
            <Route
                key="prot"
                element={<ProtectedRoute />}
            >
                {protectedRoutes}
            </Route>
        ]
    }, [routes])

    useEffect(() => {
        if (appTitle)
            document.title = appTitle
    }, [appTitle])

    useEffect(() => {
        handleGetUser()
    }, [handleGetUser])

    return (
        <div className={styles["core-wrapper"]}>
            <Router>
                <Header
                    paths={headerPaths}
                    extraContent={extraHeaderContent}
                    appTitle={appTitle}
                    showLogo={showHeaderLogo}
                />
                <div className={styles["core-content"]}>
                    <Routes>
                        {handleRoutes}
                    </Routes>
                    {!hideFooter && (
                        <Footer />
                    )}
                </div>
            </Router>
        </div>
    )
}
Core.propTypes = propTypes
Core.defaultProps = defaultProps

export default Core