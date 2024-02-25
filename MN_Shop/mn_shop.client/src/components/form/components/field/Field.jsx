import PropTypes from "prop-types"
import { Field as RcField } from "rc-field-form"

const propTypes = {
    className: PropTypes.string,
    name: PropTypes.string,
    initialValue: PropTypes.any,
    rules: PropTypes.array,
    label: PropTypes.string,
    required: PropTypes.bool,
    errors: PropTypes.array
}
const defaultProps = {}

const Field = ({
    children,
    className,
    label,
    required,
    errors,
    ...rest
}) => {
    return (
        <div
            className={className}
            aria-label={label}
            aria-required={required}
        >
            <span
                aria-errormessage={errors?.[0]}
            >
                <RcField
                    {...rest}
                >
                    {children}
                </RcField>
            </span>
        </div>
    )
}
Field.propTypes = propTypes
Field.defaultProps = defaultProps

export default Field