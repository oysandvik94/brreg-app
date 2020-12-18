import React, {useEffect, useState} from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import TableSortLabel from "@material-ui/core/TableSortLabel";
import * as PropTypes from "prop-types";
import {stableSort} from "../utils/Sorting";
import Collapse from "@material-ui/core/Collapse";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import KeyboardArrowDownIcon from '@material-ui/icons/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@material-ui/icons/KeyboardArrowUp';
import AddForm from "./AddForm";
import {NotesPopup} from "./NotesPopup";
import {FinancesPopup} from "./FinancesPopup";

const useStyles = makeStyles({
    table: {
        minWidth: 650,
    },
    visuallyHidden: {
        border: 0,
        clip: 'rect(0 0 0 0)',
        height: 1,
        margin: -1,
        overflow: 'hidden',
        padding: 0,
        position: 'absolute',
        top: 20,
        width: 1,
    },
    root: {
        '& > *': {
            borderBottom: 'unset',
        },
    }
});

// Headers and column definition for organizations
const headCells = [
    { id: 'orgnr', numeric: true, label: 'Organisasjonsnummer' },
    { id: 'orgname', numeric: false, label: 'Navn/foretaksnavn' },
    { id: 'orgtype', numeric: false, label: 'Organisasjonsform' },
    { id: 'municipality', numeric: false, label: 'Kommune' },
    { id: 'note', numeric: false, popup: true, label: 'Notat' },
    { id: 'finances', numeric: false, label: 'Finanser' }
];

// Headers and column definition for subOrganizations
const subOrgHeadCells = [
    { id: 'suborgnr', numeric: true, label: 'Organisasjonsnummer' },
    { id: 'suborgname', numeric: false, label: 'Navn/foretaksnavn' },
    { id: 'municipality', numeric: false, label: 'Kommune' }
];

// Main method for loading component
export function OrganizationTable(props) {
    const [data, setData] = useState([]);
    const [order, setOrder] = React.useState("asc");
    const [orderBy, setOrderBy] = React.useState("orgnr");
    const [loading, setLoading] = React.useState(false);
    const classes = useStyles();

    const handleRequestSort = (event, property) => {
        const isAsc = orderBy === property && order === "asc";
        setOrder(isAsc ? 'desc' : 'asc');
        setOrderBy(property);
    };
    
    // Fetch data async
    useEffect(() => {
        setLoading(true);
        fetchData()
            .then(d => {
                setData(d);
                setLoading(false);
            });
    }, [])
    
    const handleSave = async () => {
        await fetchData().then( d => {
            setData(d);
        });
    }
    
    if (loading) {
        return (
            <p><em>Laster inn...</em></p>        
        );
    }
    
    // Data has been fetched, render table
    return (
       <TableContainer component={Paper}>
           <AddForm onSave={handleSave} />
           <Table className={classes.table} aria-label="simple table">
               <SortableTableHeader
                   classes={classes}
                   order={order}
                   orderBy={orderBy}
                   onRequestSort={handleRequestSort}
               />
               <TableBody>
                       {stableSort(data, order, orderBy)
                       .map((row) => {
                           return (
                               <Row key={row.orgnr} row={row} />
                           );
                       })}
               </TableBody>
           </Table>
       </TableContainer>
    );
}

// Custom header element for adding sorting
function SortableTableHeader(props) {
    const { classes, order, orderBy, onRequestSort } = props;
    const createSortHandler = (property) => (event) => {
        onRequestSort(event, property);
    };

    return (
        <TableHead>
            <TableRow>
                {headCells.map((headCell) => (
                    <TableCell
                        key={headCell.id}
                        align={headCell.numeric ? 'left' : 'right'}
                        sortDirection={orderBy === headCell.id ? order : false}
                    >
                        <TableSortLabel
                            active={orderBy === headCell.id}
                            direction={orderBy === headCell.id ? order : 'asc'}
                            onClick={createSortHandler(headCell.id)}
                        >
                            {headCell.label}
                            {orderBy === headCell.id ? (
                                <span className={classes.visuallyHidden}>
                                  {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                                </span>
                            ) : null}
                        </TableSortLabel>
                    </TableCell>
                ))}
                <TableCell/>{/* Empty header cell for expand element */}
            </TableRow>
        </TableHead>
    );
}

SortableTableHeader.propTypes = {
    classes: PropTypes.object.isRequired,
    onRequestSort: PropTypes.func.isRequired,
    order: PropTypes.oneOf(["asc", "desc"]).isRequired,
    orderBy: PropTypes.string.isRequired,
};

// Custom Row element for collapsible function
function Row(props) {
    const { row } = props;
    const [open, setOpen] = React.useState(false);
    const [openNotes, setOpenNotes] = React.useState(false);
    const [openFinances, setOpenFinances] = React.useState(false);
    const [finances, setFinances] = React.useState(false);
    const classes = useStyles();
    return (
        <React.Fragment>
            <TableRow className={classes.root}>
                {headCells.map((cell) => (
                    <TableCell
                        key={cell.id}
                        component="th"
                        align={cell.numeric ? 'left' : 'right'}
                        scope="row"
                    >
                        {/* Open plaintext if not popup */}
                        {!cell.popup ? row[cell.id] : null}
                        
                        {cell.id === "note" ? (
                            <NotesPopup
                                openNotes={openNotes}
                                setOpenNotes={setOpenNotes}
                                valueNotes={row[cell.id]}
                            />
                        ) : null}
                        
                        {cell.id === "finances" ? (
                            <FinancesPopup
                                openFinances={openFinances}
                                setOpenFinances={setOpenFinances}
                                finances={finances}
                                orgNr={row["orgnr"]}
                                setFinances={setFinances}
                            />
                        ) : null}
                    </TableCell>
                ))}
                <TableCell>
                    <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
                        {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                    </IconButton>
                </TableCell>
            </TableRow>
            <TableRow>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box margin={1}>
                            <Typography variant="h6" gutterBottom component="div">
                                Underenheter
                            </Typography>
                            <Table size="small" aria-label="purchases">
                                <TableHead>
                                    <TableRow>
                                        {subOrgHeadCells.map((headCell) => (
                                            <TableCell
                                                key={headCell.id}
                                                align={headCell.numeric ? 'left' : 'right'}
                                            >
                                                {headCell.label}
                                            </TableCell>
                                        ))}
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {row.suborganizations.map((subOrgRow) => (
                                        <TableRow key={subOrgRow.suborgnr}>
                                            {subOrgHeadCells.map((cell) => (
                                                <TableCell
                                                    key={cell.id}
                                                    component="th"
                                                    align={cell.numeric ? 'left' : 'right'}
                                                    scope="row"
                                                >
                                                    {subOrgRow[cell.id]}
                                                </TableCell>
                                            ))}
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </React.Fragment>
    );
}

async function fetchData() {
    const response = await fetch('organizations?includeSubOrgs=true');
    return await response.json();
}
