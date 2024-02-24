import PropTypes from "prop-types"
import classNames from "classnames"

import styles from "./Input.module.css"

const propTypes = {
    className: PropTypes.string
}
const defaultProps = {}

const Input = ({
    className,
    ...rest
}) => {
    return (
        <input
            className={classNames(
                styles["mn-input"],
                className
            )}
            type="text"
            {...rest}
        />
    )
}
Input.propTypes = propTypes
Input.defaultProps = defaultProps

export default Input