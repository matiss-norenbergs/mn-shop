import PropTypes from "prop-types"
import classNames from "classnames"

import styles from "./TextArea.module.css"

const propTypes = {
    className: PropTypes.string,
    password: PropTypes.bool,
    rows: PropTypes.number
}
const defaultProps = {
    rows: 3
}

const TextArea = ({
    className,
    rows
}) => {
    return (
        <textarea
            className={classNames(
                styles["mn-text-area"],
                className
            )}
            rows={rows}
        />
    )
}
TextArea.propTypes = propTypes
TextArea.defaultProps = defaultProps

export default TextArea