import PropTypes from "prop-types"

import classNames from "classnames"
import { NavLink } from "react-router-dom"

import Button from "../button"
import MNIcon from "../mnIcon"
import FaIcon from "../faIcon"
import ThemeSwitch from "../themeSwitch"

import styles from "./Header.module.css"

const propTypes = {
    paths: PropTypes.array,
    extraContent: PropTypes.node
}
const defaultProps = {
    paths: []
}

const Header = ({
    paths,
    extraContent
}) => (
    <header className={styles["header-wrapper"]}>
        <MNIcon className={styles["header-logo"]} />
        <div className={styles["header-contents"]}>
            {paths.length > 0 && (
                <nav className={styles["navigation"]}>
                    <FaIcon
                        className={styles["nav-menu"]}
                        icon="bars"
                        fixedWidth
                    />
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
            {extraContent}
            <ThemeSwitch />
        </div>
    </header>
)
Header.propTypes = propTypes
Header.defaultProps = defaultProps

export default Header