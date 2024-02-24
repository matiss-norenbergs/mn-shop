import PropTypes from "prop-types"

import classNames from "classnames"
import { NavLink } from "react-router-dom"

import Button from "../button"
import MNIcon from "../mnIcon"
import FaIcon from "../faIcon"
import ThemeSwitch from "../themeSwitch"
import Heading from "../heading"

import styles from "./Header.module.css"

const propTypes = {
    paths: PropTypes.array,
    extraContent: PropTypes.node,
    appTitle: PropTypes.string,
    showLogo: PropTypes.bool
}
const defaultProps = {
    paths: [],
    showLogo: true
}

const Header = ({
    paths,
    extraContent,
    appTitle,
    showLogo
}) => (
    <header className={styles["header-wrapper"]}>
        <div className={styles["header-left-side"]}>
            {showLogo && (
                <MNIcon className={styles["header-logo"]} />
            )}
            <Heading
                className={styles["header-app-title"]}
                level={1}
            >
                {appTitle}
            </Heading>
        </div>
        {extraContent}
        <div className={styles["header-contents"]}>
            {paths.length > 0 && (
                <nav className={styles["navigation"]}>
                    <span className={styles["nav-menu"]}>
                        <FaIcon
                            icon="bars"
                            fixedWidth
                        />
                    </span>
                    <div className={styles["nav-paths"]}>
                        {paths.map(({ path, title, icon }) => (
                            <NavLink
                                key={path}
                                className={({ isActive }) => classNames({
                                    [styles["active-link"]]: isActive
                                })}
                                to={path}
                            >
                                <Button
                                    className={styles["link-btn"]}
                                    type="ghost"
                                    faIcon={icon}
                                >
                                    {title}
                                </Button>
                            </NavLink>
                        ))}
                    </div>
                </nav>
            )}
            <ThemeSwitch />
        </div>
    </header>
)
Header.propTypes = propTypes
Header.defaultProps = defaultProps

export default Header