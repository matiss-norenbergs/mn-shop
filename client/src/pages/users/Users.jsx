import { useCallback, useEffect, useRef, useState } from "react"
import axios from "axios"

import Layout from "components/layout"
import Table from "components/table"
import Button from "components/button"
import ModalComponent from "components/modalComponent"
import UserForm from "./components/userForm"

import { deleteUserData, getUserListData, respStatus } from "helpers/axios/userService"

const columns = [
    {
        field: "select",
        name: "",
        width: 30
    },
    {
        field: "name",
        name: "Name",
        width: 100
    },
    {
        field: "surname",
        name: "Surname",
        width: 150
    },
    {
        field: "email",
        name: "Email",
        width: 120
    }
]

const Users = () => {
    const [data, setData] = useState([])
    const [selectedRows, setSelectedRows] = useState([])

    const userFormModalElementRef = useRef(null)
    const axiosCancelToken = useRef(null)

    const getUsers = useCallback(() => {
        getUserListData(axiosCancelToken.current.token)
            .then(response => {
                if (!!response && response.status === respStatus.success) {
                    setData(response.data)
                }
            })
    }, [axiosCancelToken])

    const handleCreateClick = useCallback(() => {
        userFormModalElementRef.current?.open()
    }, [])

    const handleDeleteClick = useCallback(() => {
        if (selectedRows.length !== 1)
            return

        const postParams = {
            id: selectedRows[0].id
        }

        deleteUserData(postParams, axiosCancelToken.current.token)
            .then(response => {
                if (!!response && response.status === 204) {
                    getUsers()
                }
            })
    }, [selectedRows, getUsers])

    useEffect(() => {
        axiosCancelToken.current = axios.CancelToken.source()

        getUsers()
    }, [getUsers])

    useEffect(() => {
        return () => axiosCancelToken.current?.cancel
    }, [])

    const toolbar = (
        <Button.Group>
            <Button onClick={handleCreateClick}>
                {"Create"}
            </Button>
            <Button>
                {"Edit"}
            </Button>
            <Button onClick={handleDeleteClick}>
                {"Delete"}
            </Button>
        </Button.Group>
    )

    return (
        <Layout>
            <Table
                toolbar={toolbar}
                columns={columns}
                data={data}
                getSelectedRows={setSelectedRows}
            />
            <ModalComponent
                ref={userFormModalElementRef}
                component={<UserForm />}
                title="Create user"
                confirmText="Save"
                onConfirm={getUsers}
            />
        </Layout>
    )
}

export default Users