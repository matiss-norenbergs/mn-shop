import PropTypes from "prop-types"
import { NavLink, Outlet } from "react-router-dom"
import classNames from "classnames"

import Button from "../../../button"

import styles from "./Admin.module.css"

const propTypes = {
    paths: PropTypes.array
}
const defaultProps = {
    paths: []
}

const Admin = ({
    paths
}) => {
    return (
        <div className={styles["wrapper"]}>
            <div className={styles["navigation"]}>
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
            <div className={styles["content"]}>
                <Outlet />
            </div>
        </div>
    )
}
Admin.propTypes = propTypes
Admin.defaultProps = defaultProps

export default Admin