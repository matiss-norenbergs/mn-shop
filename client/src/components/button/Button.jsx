import PropTypes from "prop-types"
import classNames from "classnames"

import FaIcon from "../faIcon"

import Group from "./components/group"

import styles from "./Button.module.css"

const buttonTypes = {
    default: "default",
    primary: "primary",
    ghost: "ghost"
}

const propTypes = {
    active: PropTypes.bool,
    children: PropTypes.node,
    className: PropTypes.string,
    disabled: PropTypes.bool,
    faIcon: PropTypes.oneOfType([
        PropTypes.string,
        PropTypes.arrayOf(PropTypes.string)
    ]),
    onClick: PropTypes.func,
    type: PropTypes.oneOf(Object.keys(buttonTypes))
}
const defaultProps = {
    type: buttonTypes.default
}

const Button = ({
    active,
    children,
    className,
    faIcon,
    type,
    ...rest
}) => {
    const buttonStyles = classNames(
        styles["button-wrapper"],
        styles[`${type}`],
        {
            [styles["active"]]: active && !rest.disabled,
            [styles["disabled"]]: rest.disabled
        },
        className
    )

    return (
        <button
            className={buttonStyles}
            {...rest}
        >
            {faIcon && (
                <FaIcon
                    icon={faIcon}
                    padded={!!children}
                />
            )}
            {children}
        </button>
    )
}
Button.propTypes = propTypes
Button.defaultProps = defaultProps

Button.Group = Group

export default Button