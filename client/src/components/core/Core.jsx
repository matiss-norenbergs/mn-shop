import PropTypes from "prop-types"
import { useEffect } from "react"

import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import { library } from "@fortawesome/fontawesome-svg-core"
import { far } from "@fortawesome/free-regular-svg-icons"
import { fas } from "@fortawesome/free-solid-svg-icons"

import Header from "../header"
import Footer from "../footer"

import styles from "./Core.module.css"

library.add(far, fas)

const propTypes = {
    routes: PropTypes.array,
    hideFooter: PropTypes.bool,
    extraHeaderContent: PropTypes.node,
    appTitle: PropTypes.string,
    showHeaderLogo: PropTypes.bool
}
const defaultProps = {}

const Core = ({
    routes,
    hideFooter,
    extraHeaderContent,
    appTitle,
    showHeaderLogo
}) => {
    const headerPaths = routes.filter(({ menuHidden }) => !menuHidden).map(({ path, title, icon }) => ({
        path,
        title,
        icon
    }))

    useEffect(() => {
        if (appTitle)
            document.title = appTitle
    }, [appTitle])

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
                        { routes.map(({ path, element: Element }) => (
                            <Route
                                key={path}
                                exact={path === "/"}
                                path={path}
                                element={<Element />}
                            />
                        ))}
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