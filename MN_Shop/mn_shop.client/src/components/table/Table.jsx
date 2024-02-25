import PropTypes from "prop-types"

import { useCallback, useEffect, useState } from "react"

import SelectedRenderer from "./components/selectedRenderer"

import styles from "./Table.module.css"
import classNames from "classnames"

const columnAlign = {
    left: "left",
    right: "right",
    center: "center"
}

const propTypes = {
    columns: PropTypes.arrayOf(PropTypes.shape({
        field: PropTypes.string,
        name: PropTypes.string,
        width: PropTypes.number,
        align: PropTypes.oneOf(Object.keys(columnAlign)),
        cellRenderer: PropTypes.string
    })),
    data: PropTypes.array,
    rowKey: PropTypes.string,
    cellRenderers: PropTypes.object,
    getSelectedRows: PropTypes.func,
    toolbar: PropTypes.node
}
const defaultProps = {
    columns: [],
    data: [],
    rowKey: "id",
    cellRenderers: {}
}

const Table = ({
    columns,
    data,
    rowKey,
    cellRenderers,
    getSelectedRows,
    toolbar
}) => {
    const [targetKeys, setTargetKeys] = useState([])

    const handleRowClick = useCallback((id) => () => {
        setTargetKeys(prevState => {
            const currentKeys = [...prevState]

            if (currentKeys.includes(id))
                currentKeys.splice(currentKeys.indexOf(id), 1)
            else
                currentKeys.push(id)

            return currentKeys
        })
    }, [])

    const getCellRenderer = useCallback((CellRenderer, cellValue, rowData) => (
        <CellRenderer
            value={cellValue}
            data={rowData}
        />
    ), [])

    useEffect(() => {
        if (getSelectedRows)
            getSelectedRows(data.filter(row => targetKeys.includes(row[rowKey])))
    }, [targetKeys, data, rowKey, getSelectedRows])

    return (
        <div className={styles["table-outer-wrapper"]}>
            {toolbar && (
                <div className={styles["toolbar-wrapper"]}>
                    {toolbar}
                </div>
            )}
            <table className={styles["table-wrapper"]}>
                <thead>
                    <tr>
                        {columns.map(({
                            field,
                            name,
                            width,
                            align
                        }) => (
                            <th
                                key={field}
                                className={styles[align]}
                                style={{ width }}
                            >
                                {name}
                            </th>
                        ))}
                    </tr>
                </thead>
                <tbody>
                    {data.map((row) => (
                        <tr
                            key={row[rowKey]}
                            onClick={handleRowClick(row[rowKey])}
                        >
                            {columns.map(({
                                field,
                                name,
                                align,
                                cellRenderer
                            }) => (
                                field === "select" ? (
                                    <td
                                        key={field}
                                        className={classNames(
                                            styles[columnAlign.center],
                                            styles["select-cell"]
                                        )}
                                    >
                                        <SelectedRenderer selected={targetKeys.includes(row[rowKey])} />
                                    </td>
                                ) : (
                                    <td
                                        key={field}
                                        className={styles[align]}
                                        data-cell={name}
                                    >
                                        {Object.keys(cellRenderers).includes(cellRenderer) ? (
                                            getCellRenderer(cellRenderers[cellRenderer], row[field], row)
                                        ) : (
                                            row[field]
                                        )}
                                    </td>
                                )
                            ))}
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}
Table.propTypes = propTypes
Table.defaultProps = defaultProps

export default Table