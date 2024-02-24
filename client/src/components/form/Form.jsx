import PropTypes from "prop-types"
import { Children, cloneElement } from "react"
import Form from "rc-field-form"

import Field from "./components/field"

import styles from "./Form.module.css"

const propTypes = {
    form: PropTypes.object
}
const defaultProps = {}

const MyForm = ({
    form,
    children
}) => {
    return (
        <Form
            className={styles["mn-form"]}
            form={form}
        >
            {(values, form) => {
                const fieldErrors = {}
                Object.keys(values).forEach(fieldName => {
                    fieldErrors[fieldName] = form.getFieldError(fieldName)
                })

                return Children.map(children, (element) => {
                    if (element.type === Field) {
                        return cloneElement(element, {
                            className: styles["form-field"],
                            errors: fieldErrors[element.props.name]
                        })
                    }

                    return element
                })
            }}
        </Form>
    )
}
MyForm.propTypes = propTypes
MyForm.defaultProps = defaultProps

MyForm.useForm = Form.useForm
MyForm.Field = Field
MyForm.FormProvider = Form.FormProvider

export default MyForm