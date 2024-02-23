import PropTypes from "prop-types"

import styles from "./Layout.module.css"

const layoutTypes = {
    basic: "basic-layout"
}

const propTypes = {
    children: PropTypes.node,
    layoutType: PropTypes.oneOf(Object.values(layoutTypes))
}
const defaultProps = {
    layoutType: layoutTypes.basic
}

const Layout = ({
    children,
    layoutType
}) => {
    return (
        <div className={styles[layoutType]}>
            {children}
        </div>
    )
}
Layout.propTypes = propTypes
Layout.defaultProps = defaultProps

export default Layout