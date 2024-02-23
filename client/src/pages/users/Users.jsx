import { useCallback, useEffect, useRef, useState } from "react"
import axios from "axios"

import Layout from "components/layout"
import Table from "components/table"
import Button from "components/button"
import ModalComponent from "components/modalComponent"
import UserForm from "./components/userForm"

import { getUserListData, respStatus } from "helpers/axios/userService"

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

    const userFormModalElementRef = useRef()

    const getUsers = useCallback(() => {
        const source = axios.CancelToken.source()

        getUserListData(source.token)
            .then(response => {
                if (!!response && response.status === respStatus.success) {
                    setData(response.data)
                }
            })

        return () => source.cancel
    }, [])

    const handleCreateClick = () => {
        userFormModalElementRef.current?.open()
    }

    useEffect(() => {
        getUsers()
    }, [getUsers])

    const toolbar = (
        <Button.Group>
            <Button onClick={handleCreateClick}>
                {"Create"}
            </Button>
            <Button>
                {"Edit"}
            </Button>
            <Button>
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
            />
            <ModalComponent
                ref={userFormModalElementRef}
                component={<UserForm />}
                title="Create user"
                confirmText="Save"
            />
        </Layout>
    )
}

export default Users